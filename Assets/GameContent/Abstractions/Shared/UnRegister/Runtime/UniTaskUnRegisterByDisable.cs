using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnRegister
{
    public class UniTaskUnRegisterByDisable : MonoBehaviour
    {
        private Stack<CancellationTokenSource> cts = new();
        public Stack<CancellationTokenSource> CTS => cts;

        public CancellationTokenSource Register()
        {
            var t = new CancellationTokenSource();
            cts.Push(t);
            return t;
        }
        private void OnDisable()
        {
            while (cts.TryPop(out var t))
            {
                t.Cancel();
            }
        }
    }
}
