using System;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    void Execute(Player player);
}

public class InputManager : MonoBehaviour
{
    private Dictionary<KeyCode, Command> keyDownCommands = new Dictionary<KeyCode, Command>();
    private Dictionary<KeyCode, Command> keyHoldCommands = new Dictionary<KeyCode, Command>();

    private Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
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
        AttackCommand attack = new AttackCommand();
        CrouchCommand crouch = new CrouchCommand();

        BindKey(KeyCode.W, jump, keyDownCommands);
        BindKey(KeyCode.A, moveLeft, keyDownCommands);
        BindKey(KeyCode.D, moveRight, keyDownCommands);
        BindKey(KeyCode.L, attack, keyDownCommands);
        BindKey(KeyCode.LeftShift, crouch, keyHoldCommands);
    }

    void Update()
    {
        foreach (var binding in keyDownCommands)
        {
            if (Input.GetKey(binding.Key))
            {
                binding.Value?.Execute(player);
            }
        }

        foreach (var binding in keyHoldCommands)
        {
            if (Input.GetKeyDown(binding.Key) || Input.GetKeyUp(binding.Key))
            {
                binding.Value?.Execute(player);
            }
        }
    }
}

public class JumpCommand : Command
{
    public void Execute(Player player)
    {
        player.Jump();
    }
}

public class MoveLeftCommand : Command
{
    public void Execute(Player player)
    {
        player.MoveLeft();
    }
}

public class MoveRightCommand : Command
{
    public void Execute(Player player)
    {
        player.MoveRight();
    }
}

public class AttackCommand : Command
{
    public void Execute(Player player)
    {
        player.Attack();
    }
}

public class CrouchCommand : Command
{
    public void Execute(Player player)
    {
        player.Crouch();
    }
}
