using System.Collections.Generic;


namespace MarketBot.Date
{
    namespace QuickType
    {

        public partial struct SteamList
        {
            public bool Success { get; set; }
            public string Currency { get; set; }
            public long Timestamp { get; set; }
            public Dictionary<string, ItemsList> ItemsList { get; set; }
        }

        public partial struct ItemsList
        {
            public string Name { get; set; }
            public long Marketable { get; set; }
            public long Tradable { get; set; }
            public string Classid { get; set; }
            public string IconUrl { get; set; }
            public string IconUrlLarge { get; set; }
            public TypeEnum? Type { get; set; }
            public Rarity Rarity { get; set; }
            public RarityColor RarityColor { get; set; }
            public Price Price { get; set; }
            public long? FirstSaleDate { get; set; }
            public WeaponType? WeaponType { get; set; }
            public GunType? GunType { get; set; }
            public Exterior? Exterior { get; set; }
            public KnifeType? KnifeType { get; set; }
            public long? Souvenir { get; set; }
            public Tournament? Tournament { get; set; }
            public long? Stattrak { get; set; }
            public long? Sticker { get; set; }
        }

        public partial struct Price
        {
            public The24__Hours The24_Hours { get; set; }
            public The24__Hours The7_Days { get; set; }
            public The24__Hours The30_Days { get; set; }
            public The24__Hours AllTime { get; set; }
        }

        public partial struct The24__Hours
        {
            public double Average { get; set; }
            public double Median { get; set; }
            public string Sold { get; set; }
            public string StandardDeviation { get; set; }
            public double LowestPrice { get; set; }
            public double HighestPrice { get; set; }
        }

        public enum Exterior { BattleScarred, FactoryNew, FieldTested, MinimalWear, NotPainted, WellWorn };

        public enum GunType { Ak47, Aug, Awp, Cz75Auto, DesertEagle, DualBerettas, Famas, FiveSeveN, G3Sg1, GalilAr, Glock18, M249, M4A1S, M4A4, Mac10, Mag7, Mp5Sd, Mp7, Mp9, Negev, Nova, P2000, P250, P90, PpBizon, R8Revolver, SawedOff, Scar20, Sg553, Ssg08, Tec9, Ump45, UspS, Xm1014 };

        public enum KnifeType { Bayonet, BowieKnife, ButterflyKnife, ClassicKnife, FalchionKnife, FlipKnife, GutKnife, HuntsmanKnife, Karambit, M9Bayonet, NavajaKnife, NomadKnife, ParacordKnife, ShadowDaggers, SkeletonKnife, StilettoKnife, SurvivalKnife, TalonKnife, UrsusKnife };

        public enum Rarity { BaseGrade, Classified, ConsumerGrade, Contraband, Covert, Distinguished, Exceptional, Exotic, Extraordinary, HighGrade, IndustrialGrade, Master, MilSpecGrade, Remarkable, Restricted, Superior };

        public enum RarityColor { B0C3D9, D32Ce6, E4Ae39, Eb4B4B, The4B69Ff, The5E98D9, The8847Ff };

        public enum Tournament { The2013DreamHackWinter, The2014DreamHackWinter, The2014EmsOneKatowice, The2014EslOneCologne, The2015DreamHackClujNapoca, The2015EslOneCologne, The2015EslOneKatowice, The2016EslOneCologne, The2016MlgColumbus, The2017EleagueAtlanta, The2017PglKrakow, The2018FaceitLondon, The2019IemKatowice, The2019StarLadderBerlin, The2020Rmr, The2021PglStockholm };

        public enum TypeEnum { Collectible, Container, Gift, Gloves, Graffiti, Key, MusicKit, Pass, Weapon };

        public enum WeaponType { Knife, Machinegun, Pistol, Rifle, Shotgun, Smg, SniperRifle };
    }

}


