using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Core.DI;
using UnityEngine;
using ILogger = Assets.Abstractions.Shared.Core.ILogger;
namespace Assets.Abstractions
{
    internal class TestInstaller : MonoBehaviour
    {
        [Inject] private ILogger _logger;

        private void Start()
        {
            _logger.Info("Test Inject");
        }
    }
}
