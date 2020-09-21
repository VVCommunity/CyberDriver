using EasyButtons;
using Core.Entities.Pursuit;
using System;
using UnityEngine;

namespace Core
{
    public class PursuitManager : MonoBehaviour
    {
        public static event Action<PursuitSteps> OnPursuitStepChanged;

        [SerializeField]
        private PursuitSteps SelectedPursuitStepFromEditor;
        public static PursuitSteps PursuitStep { get; private set; }

        [SerializeField]
        private double SecondsToReduceStepFromEditor;
        public static double SecondsToReduceStep { get; private set; }
        private static double remainingSecondsToReduceStep;

        public void Awake()
        {
            PursuitStep = SelectedPursuitStepFromEditor;
            SecondsToReduceStep = SecondsToReduceStepFromEditor;
        }

        public void UpdateFixed()
        {
            remainingSecondsToReduceStep -= Time.fixedDeltaTime;
            if (remainingSecondsToReduceStep <= 0)
            {
                if (PursuitStep != PursuitSteps.Free)
                {
                    --PursuitStep;
                    OnPursuitStepChanged?.Invoke(PursuitStep);
                }
                remainingSecondsToReduceStep += SecondsToReduceStep;
            }
        }

        [Button]
        public static void PreviousPursuitStep()
        {
            if (PursuitStep != PursuitSteps.Free)
            {
                --PursuitStep;
                OnPursuitStepChanged?.Invoke(PursuitStep);
                remainingSecondsToReduceStep = SecondsToReduceStep;
            }
        }

        [Button]
        public static void NextPursuitStep()
        {
            if (PursuitStep != PursuitSteps.Caught)
            {
                ++PursuitStep;
                OnPursuitStepChanged?.Invoke(PursuitStep);
                remainingSecondsToReduceStep = SecondsToReduceStep;
            }
        }
    }
}
