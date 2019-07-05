﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class BowBottlePrimitive
    {
        public readonly byte CloseRange;
        public readonly byte Power;
        public readonly byte Paralysis;
        public readonly byte Poison;
        public readonly byte Sleep;
        public readonly byte Blast;

        private BowBottlePrimitive(
            byte closeRange,
            byte power,
            byte paralysis,
            byte poison,
            byte sleep,
            byte blast
        )
        {
            CloseRange = closeRange;
            Power = power;
            Paralysis = paralysis;
            Poison = poison;
            Sleep = sleep;
            Blast = blast;
        }

        public static BowBottlePrimitive Read(Reader reader)
        {
            byte closeRange = reader.ReadByte();
            byte power = reader.ReadByte();
            byte paralysis = reader.ReadByte();
            byte poison = reader.ReadByte();
            byte sleep = reader.ReadByte();
            byte blast = reader.ReadByte();

            return new BowBottlePrimitive(closeRange, power, paralysis, poison, sleep, blast);
        }
    }
}
