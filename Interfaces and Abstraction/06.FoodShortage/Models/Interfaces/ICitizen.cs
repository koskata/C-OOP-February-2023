﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl.Models.Interfaces
{
    public interface ICitizen
    {
        string Name { get; }
        int Age { get; }
        string Id { get; }
        string Birthdate { get; }
    }
}
