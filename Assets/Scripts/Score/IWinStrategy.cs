using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crossout.Scoring
{
    public interface IWinStrategy
    {
        public bool EvaluateWin(ScoreData score);
    }
}
