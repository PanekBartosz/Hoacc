﻿namespace HoaccIServices.Requests;

public class CreateUser
{
    public int UserId { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}