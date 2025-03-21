using BansheeGz.BGDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Databases
{
    public class ConfigTable : DataTableAsset<string, ConfigData>
    {
        public override string TableName => "ConfigTable";

        protected override string GetId(in ConfigData data)
        {
            return data.Id;
        }
        public override void Initialize()
        {
            base.Initialize();
            List<ConfigData> config = new();
            DB_GameConfig.ForEachEntity(e => Get(e));
            void Get(DB_GameConfig e)
            {
                config.Add(new ConfigData(e.id, e.value));
            }
            SetEntries(config.ToArray());
        }
    }
}
