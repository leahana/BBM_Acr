using AEAssist.CombatRoutine;

namespace BBM.GCD
{
    public class NinSpellsHelper
    {
        public const uint 双刃旋 = SpinningEdge;
        public const uint 绝风 = GustSlash;
        public const uint 旋风刃 = AeolianEdge;

        public static readonly HashSet<uint> NinAbilityAsGcdSet =
        [
            双刃旋,
            绝风,
            旋风刃
        ];

        public const uint SpinningEdge = 2240; //双刃旋 1
        public const uint ShadeShift = 2241; //残影
        public const uint GustSlash = 2242; //绝风 2
        public const uint Hide = 2245; // 隐遁
        public const uint Assassinate = 2246; //断绝
        public const uint ThrowingDagger = 2247; //飞刀
        public const uint Mug = 2248; // 夺取
        public const uint DeathBlossom = 2254; // 血雨飞花
        public const uint AeolianEdge = 2255; // 3 旋风刃
        public const uint ShadowFang = 2257; //

        public const uint TrickAttack = 2258; // 攻其不备

        // 天
        public const uint Ten = 2259; //天之印

        // 忍术
        public const uint Ninjutsu = 2260; // 忍术
        public const uint Chi = 2261; // 地之印
        public const uint Shukuchi = 2262; // 缩地
        public const uint Jin = 2263; // 人之印
        public const uint Kassatsu = 2264; // 生杀予夺
        public const uint FumaShuriken = 2265; // 风魔手里剑
        public const uint Katon = 2266;
        public const uint Raiton = 2267;
        public const uint Hyoton = 2268;
        public const uint Huton = 2269;
        public const uint Doton = 2270;
        public const uint Suiton = 2271;
        public const uint RabbitMedium = 2272;
        public const uint ArmorCrush = 3563;
        public const uint DreamWithinaDream = 3566;
        public const uint HellfrogMedium = 7401;
        public const uint Bhavacakra = 7402; //六道轮回
        public const uint TenChiJin = 7403;
        public const uint HakkeMujinsatsu = 16488;
        public const uint Meisui = 16489;
        public const uint GokaMekkyaku = 16491;
        public const uint HyoshoRanryu = 16492;
        public const uint Bunshin = 16493;
        public const uint PhantomKamaitachi = 25774;
        public const uint ForkedRaiju = 25777;
        public const uint FleetingRaiju = 25778;
        public const uint Huraijin = 25876;
        public const uint TenCombo = 18805;
        public const uint ChiCombo = 18806;
        public const uint JinCombo = 18807;
        public const uint TcjFumaShurikenTen = 18873;
        public const uint TcjFumaShurikenChi = 18874;
        public const uint TcjFumaShurikenJin = 18875;
        public const uint TcjKaton = 18876;
        public const uint TcjRaiton = 18877;
        public const uint TcjHyoton = 18878;
        public const uint TcjHuton = 18879;
        public const uint TcjDoton = 18880;
        public const uint TcjSuiton = 18881;
    }
}