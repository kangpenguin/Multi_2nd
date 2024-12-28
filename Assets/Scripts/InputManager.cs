using System;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    void Execute(Entity entity);
}

public class InputManager : MonoBehaviour
{
    private Dictionary<KeyCode, Command> keyDownCommands = new Dictionary<KeyCode, Command>();
    private Dictionary<KeyCode, Command> keyHoldCommands = new Dictionary<KeyCode, Command>();

    private Entity entity;

    void Awake()
    {
        entity = FindObjectOfType<Dino>();
    }

    public void BindKey(KeyCode key, Command command, Dictionary<KeyCode, Command> dictionary)
    {
        dictionary[key] = command;
    }

    void Start()
    {
        JumpCommand jump = new JumpCommand();
        MoveLeftCommand moveLeft = new MoveLeftCommand();
        MoveRightCommand moveRight = new MoveRightCommand();
        MoveAnimationCommand moveAnimation = new MoveAnimationCommand();
        AttackCommand attack = new AttackCommand();
        CrouchCommand crouch = new CrouchCommand();

        BindKey(KeyCode.W, jump, keyDownCommands);
        BindKey(KeyCode.A, moveLeft, keyDownCommands);
        BindKey(KeyCode.D, moveRight, keyDownCommands);
        BindKey(KeyCode.A, moveAnimation, keyHoldCommands);
        BindKey(KeyCode.D, moveAnimation, keyHoldCommands);
        BindKey(KeyCode.L, attack, keyDownCommands);

        BindKey(KeyCode.LeftShift, crouch, keyHoldCommands);
    }

    void Update()
    {
        foreach (var binding in keyDownCommands)
        {
            if (Input.GetKey(binding.Key))
            {
                binding.Value?.Execute(entity);
            }
        }

        foreach (var binding in keyHoldCommands)
        {
            if (Input.GetKeyDown(binding.Key) || Input.GetKeyUp(binding.Key))
            {
                binding.Value?.Execute(entity);
            }
        }
    }
}

public class JumpCommand : Command
{
    public void Execute(Entity entity)
    {
        entity.Jump();
    }
}

public class MoveLeftCommand : Command
{
    public void Execute(Entity entity)
    {
        entity.MoveLeft();
    }
}

public class MoveRightCommand : Command
{
    public void Execute(Entity entity)
    {
        entity.MoveRight();
    }
}

public class MoveAnimationCommand : Command
{
    public void Execute(Entity entity)
    {
        entity.MoveAnimation();
    }
}

public class AttackCommand : Command
{
    public void Execute(Entity entity)
    {
        entity.Attack();
    }
}

public class CrouchCommand : Command
{
    public void Execute(Entity entity)
    {
        entity.Crouch();
    }
}
