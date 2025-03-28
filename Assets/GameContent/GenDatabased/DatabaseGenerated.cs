using System;
using System.Collections.Generic;
using BansheeGz.BGDatabase;

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

#pragma warning disable 414
namespace Shared.Databases
{

	public partial class DB_GameConfig : BGEntity
	{

		public class Factory : BGEntity.EntityFactory
		{
			public BGEntity NewEntity(BGMetaEntity meta) => new DB_GameConfig(meta);
			public BGEntity NewEntity(BGMetaEntity meta, BGId id) => new DB_GameConfig(meta, id);
		}

		public static class __Names
		{
			public const string Meta = "GameConfig";
			public const string name = "name";
			public const string id = "id";
			public const string value = "value";
		}
		private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
		public static BansheeGz.BGDatabase.BGMetaRow MetaDefault => _metaDefault ?? (_metaDefault = BGCodeGenUtils.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(4910271197501862936UL,3353626354969855885UL), () => _metaDefault = null));
		public static BansheeGz.BGDatabase.BGRepoEvents Events => BGRepo.I.Events;
		public static int CountEntities => MetaDefault.CountEntities;
		public System.String name
		{
			get => _name[Index];
			set => _name[Index] = value;
		}
		public System.String id
		{
			get => _id[Index];
			set => _id[Index] = value;
		}
		public System.String value
		{
			get => _value[Index];
			set => _value[Index] = value;
		}
		private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
		public static BansheeGz.BGDatabase.BGFieldEntityName _name => _ufle12jhs77_name ?? (_ufle12jhs77_name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEntityName>(MetaDefault, new BGId(5010432086190911426UL, 4821833839282189201UL), () => _ufle12jhs77_name = null));
		private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_id;
		public static BansheeGz.BGDatabase.BGFieldString _id => _ufle12jhs77_id ?? (_ufle12jhs77_id = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldString>(MetaDefault, new BGId(5583532859295537010UL, 272146246167201674UL), () => _ufle12jhs77_id = null));
		private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_value;
		public static BansheeGz.BGDatabase.BGFieldString _value => _ufle12jhs77_value ?? (_ufle12jhs77_value = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldString>(MetaDefault, new BGId(5476195108998103522UL, 13513023426064241575UL), () => _ufle12jhs77_value = null));
		private static readonly DB_GameConfig.Factory _factory0_PFS = new DB_GameConfig.Factory();
		private static readonly DB_HeroTable.Factory _factory1_PFS = new DB_HeroTable.Factory();
		private DB_GameConfig() : base(MetaDefault) {}
		private DB_GameConfig(BGId id) : base(MetaDefault, id) {}
		private DB_GameConfig(BGMetaEntity meta) : base(meta) {}
		private DB_GameConfig(BGMetaEntity meta, BGId id) : base(meta, id) {}
		public static DB_GameConfig FindEntity(Predicate<DB_GameConfig> filter) => BGCodeGenUtils.FindEntity(MetaDefault, filter);
		public static List<DB_GameConfig> FindEntities(Predicate<DB_GameConfig> filter, List<DB_GameConfig> result=null, Comparison<DB_GameConfig> sort=null) => BGCodeGenUtils.FindEntities(MetaDefault, filter, result, sort);
		public static void ForEachEntity(Action<DB_GameConfig> action, Predicate<DB_GameConfig> filter=null, Comparison<DB_GameConfig> sort=null) => BGCodeGenUtils.ForEachEntity(MetaDefault, action, filter, sort);
		public static DB_GameConfig GetEntity(BGId entityId) => (DB_GameConfig) MetaDefault.GetEntity(entityId);
		public static DB_GameConfig GetEntity(int index) => (DB_GameConfig) MetaDefault[index];
		public static DB_GameConfig GetEntity(string entityName) => (DB_GameConfig) MetaDefault.GetEntity(entityName);
		public static DB_GameConfig NewEntity() => (DB_GameConfig) MetaDefault.NewEntity();
		public static DB_GameConfig NewEntity(BGId entityId) => (DB_GameConfig) MetaDefault.NewEntity(entityId);
		public static DB_GameConfig NewEntity(Action<DB_GameConfig> callback) => (DB_GameConfig) MetaDefault.NewEntity(new BGMetaEntity.NewEntityContext(entity => callback((DB_GameConfig)entity)));
	}

	public partial class DB_HeroTable : BGEntity
	{

		public class Factory : BGEntity.EntityFactory
		{
			public BGEntity NewEntity(BGMetaEntity meta) => new DB_HeroTable(meta);
			public BGEntity NewEntity(BGMetaEntity meta, BGId id) => new DB_HeroTable(meta, id);
		}

		public static class __Names
		{
			public const string Meta = "HeroTable";
			public const string name = "name";
			public const string Id = "Id";
			public const string Name = "Name";
		}
		private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
		public static BansheeGz.BGDatabase.BGMetaRow MetaDefault => _metaDefault ?? (_metaDefault = BGCodeGenUtils.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(4846668561103226499UL,4226422567659902123UL), () => _metaDefault = null));
		public static BansheeGz.BGDatabase.BGRepoEvents Events => BGRepo.I.Events;
		public static int CountEntities => MetaDefault.CountEntities;
		public System.String name
		{
			get => _name[Index];
			set => _name[Index] = value;
		}
		public System.Int32 Id
		{
			get => _Id[Index];
			set => _Id[Index] = value;
		}
		public System.String Name
		{
			get => _Name[Index];
			set => _Name[Index] = value;
		}
		private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
		public static BansheeGz.BGDatabase.BGFieldEntityName _name => _ufle12jhs77_name ?? (_ufle12jhs77_name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldEntityName>(MetaDefault, new BGId(4657090968261354573UL, 8413386595158904224UL), () => _ufle12jhs77_name = null));
		private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_Id;
		public static BansheeGz.BGDatabase.BGFieldInt _Id => _ufle12jhs77_Id ?? (_ufle12jhs77_Id = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldInt>(MetaDefault, new BGId(4948165802724470727UL, 10730091580450231736UL), () => _ufle12jhs77_Id = null));
		private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_Name;
		public static BansheeGz.BGDatabase.BGFieldString _Name => _ufle12jhs77_Name ?? (_ufle12jhs77_Name = BGCodeGenUtils.GetField<BansheeGz.BGDatabase.BGFieldString>(MetaDefault, new BGId(4783866430799567961UL, 5766305495639819183UL), () => _ufle12jhs77_Name = null));
		private static readonly DB_GameConfig.Factory _factory0_PFS = new DB_GameConfig.Factory();
		private static readonly DB_HeroTable.Factory _factory1_PFS = new DB_HeroTable.Factory();
		private DB_HeroTable() : base(MetaDefault) {}
		private DB_HeroTable(BGId id) : base(MetaDefault, id) {}
		private DB_HeroTable(BGMetaEntity meta) : base(meta) {}
		private DB_HeroTable(BGMetaEntity meta, BGId id) : base(meta, id) {}
		public static DB_HeroTable FindEntity(Predicate<DB_HeroTable> filter) => BGCodeGenUtils.FindEntity(MetaDefault, filter);
		public static List<DB_HeroTable> FindEntities(Predicate<DB_HeroTable> filter, List<DB_HeroTable> result=null, Comparison<DB_HeroTable> sort=null) => BGCodeGenUtils.FindEntities(MetaDefault, filter, result, sort);
		public static void ForEachEntity(Action<DB_HeroTable> action, Predicate<DB_HeroTable> filter=null, Comparison<DB_HeroTable> sort=null) => BGCodeGenUtils.ForEachEntity(MetaDefault, action, filter, sort);
		public static DB_HeroTable GetEntity(BGId entityId) => (DB_HeroTable) MetaDefault.GetEntity(entityId);
		public static DB_HeroTable GetEntity(int index) => (DB_HeroTable) MetaDefault[index];
		public static DB_HeroTable GetEntity(string entityName) => (DB_HeroTable) MetaDefault.GetEntity(entityName);
		public static DB_HeroTable NewEntity() => (DB_HeroTable) MetaDefault.NewEntity();
		public static DB_HeroTable NewEntity(BGId entityId) => (DB_HeroTable) MetaDefault.NewEntity(entityId);
		public static DB_HeroTable NewEntity(Action<DB_HeroTable> callback) => (DB_HeroTable) MetaDefault.NewEntity(new BGMetaEntity.NewEntityContext(entity => callback((DB_HeroTable)entity)));
	}
}
#pragma warning restore 414
