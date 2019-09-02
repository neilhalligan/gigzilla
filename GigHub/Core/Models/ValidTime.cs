﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Core.Models
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var isValid = DateTime.TryParseExact(Convert.ToString(value), 
                "HH:mm", 
                CultureInfo.CurrentCulture, 
                DateTimeStyles.None, 
                out DateTime _);
            return (isValid);
        }
    }
}