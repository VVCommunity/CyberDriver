using Assets.Scripts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Abstractions
{
    public interface ICargo
    {
        CargoCondition Condition { get; set; }
        int Worth { get; }
        GameObject gameObject { get; }
    }
}
