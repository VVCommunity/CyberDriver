using Core.Entities;
using UnityEngine;

namespace Core.Abstractions
{
    public interface ICargo
    {
        CargoState State { get; set; }
        int Worth { get; }
        GameObject gameObject { get; }
    }
}
