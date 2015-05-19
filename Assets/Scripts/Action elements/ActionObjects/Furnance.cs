﻿using UnityEngine;
using System.Collections;

public class Furnance : ActionE, IHandle<EndGuiDestilationMessage>, IHandle<EndTakeOil>, IHandle<ProtaEntersStructureMessage>, IHandle<ProtaExitsStructureMessage>
{

    private string _activationType;
    private int _idMessage = 0;
    private bool _destilating = false;
    private CountDown _countdown;
    private float _conversionRate = 0;


    public void SetLighthousetype(BaseCaracterStats ActivationStats)
    {
        if (ActivationStats.RedHearts != 0)
        {
            _activationType = "red";
            _conversionRate = Constants.Furnance.RedHeartTimeToOilRate;
        }
        else if (ActivationStats.BlueHearts != 0)
        {
            _activationType = "blue";
            _conversionRate = Constants.Furnance.BlueHeartTimeToOilRate;
        }
        else if (ActivationStats.YellowHearts != 0)
        {
            _activationType = "yellow";
            _conversionRate = Constants.Furnance.YellowHeartTimeToOilRate;
        }
    }

    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        if (!_destilating)
        {
            Messenger.Publish(new StartGuiDestilationMessage(_activationType, stats, _idMessage));
        }
        else
        {
            int oilDestilated;
            if (_countdown.isFinished) oilDestilated = (int)(_countdown.life * _conversionRate);
            else oilDestilated = (int) (_countdown.elapsed*_conversionRate);
            Messenger.Publish(new StartTakeOil(stats,_activationType, oilDestilated, _idMessage));
        }
       
    }
  

    private void StartDestilation(BaseCaracterStats modifiedStats)
    {
        float processTime;
        switch (_activationType)
        {

            case "red":
                processTime = -Constants.Furnance.RedHeartProcessTime * modifiedStats.RedHearts;
                break;
            case "blue":
                processTime = -Constants.Furnance.BlueHeartProcessTime * modifiedStats.BlueHearts;
                break;
            case "yellow":
                processTime = -Constants.Furnance.YellowHeartProcessTime * modifiedStats.YellowHearts;
                break;
            default:
                processTime = 0;
                break;
        }
       
        _countdown = new CountDown(processTime);
        _countdown.Start();
        _countdown.PauseToggle();
        _destilating = true;
    }

    public void Handle(EndGuiDestilationMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (message.ModifiedStats.RedHearts != 0 || message.ModifiedStats.BlueHearts != 0 ||
            message.ModifiedStats.YellowHearts != 0)
        {
            StartDestilation(message.ModifiedStats);
        }
        Messenger.Publish(new ContinueMessage());
        _idMessage = 0;
    }

    public void Handle(EndTakeOil message)
    {
        if (message.MessageId != _idMessage) return;
        if (message.IsOilCollected)
        {
            _destilating = false;
        }
        Messenger.Publish(new ContinueMessage());
    }

    public void Handle(ProtaEntersStructureMessage message)
    {
        if (_destilating)
        {
            _countdown.PauseToggle();
        }
       
    }

    public void Handle(ProtaExitsStructureMessage message)
    {
        if (_destilating)
        {
            _countdown.PauseToggle();
        } 
    }
}

