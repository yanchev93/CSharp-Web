﻿namespace CarShop.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string IsMechanic = "Mechanic";
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int CarMinModel = 5;
        public const int CarMinYear = 1900;
        public const int CarMaxYear = 2050;
        public const string CarPlateNumberRegEx = @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$";
    }
}
