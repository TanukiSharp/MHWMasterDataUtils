using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class AmmoTableEntryPrimitive
    {
        public readonly AmmoEntryPrimitive Normal1;
        public readonly AmmoEntryPrimitive Normal2;
        public readonly AmmoEntryPrimitive Normal3;
        public readonly AmmoEntryPrimitive Pierce1;
        public readonly AmmoEntryPrimitive Pierce2;
        public readonly AmmoEntryPrimitive Pierce3;
        public readonly AmmoEntryPrimitive Spread1;
        public readonly AmmoEntryPrimitive Spread2;
        public readonly AmmoEntryPrimitive Spread3;
        public readonly AmmoEntryPrimitive Cluster1;
        public readonly AmmoEntryPrimitive Cluster2;
        public readonly AmmoEntryPrimitive Cluster3;
        public readonly AmmoEntryPrimitive Wyvern;
        public readonly AmmoEntryPrimitive Sticky1;
        public readonly AmmoEntryPrimitive Sticky2;
        public readonly AmmoEntryPrimitive Sticky3;
        public readonly AmmoEntryPrimitive Slicing;
        public readonly AmmoEntryPrimitive Flaming;
        public readonly AmmoEntryPrimitive Water;
        public readonly AmmoEntryPrimitive Freeze;
        public readonly AmmoEntryPrimitive Thunder;
        public readonly AmmoEntryPrimitive Dragon;
        public readonly AmmoEntryPrimitive Poison1;
        public readonly AmmoEntryPrimitive Poison2;
        public readonly AmmoEntryPrimitive Paralysis1;
        public readonly AmmoEntryPrimitive Paralysis2;
        public readonly AmmoEntryPrimitive Sleep1;
        public readonly AmmoEntryPrimitive Sleep2;
        public readonly AmmoEntryPrimitive Exhaust1;
        public readonly AmmoEntryPrimitive Exhaust2;
        public readonly AmmoEntryPrimitive Recover1;
        public readonly AmmoEntryPrimitive Recover2;
        public readonly AmmoEntryPrimitive Demon;
        public readonly AmmoEntryPrimitive Armor;
        public readonly AmmoEntryPrimitive Tranq;

        private AmmoTableEntryPrimitive(
            AmmoEntryPrimitive normal1,
            AmmoEntryPrimitive normal2,
            AmmoEntryPrimitive normal3,
            AmmoEntryPrimitive pierce1,
            AmmoEntryPrimitive pierce2,
            AmmoEntryPrimitive pierce3,
            AmmoEntryPrimitive spread1,
            AmmoEntryPrimitive spread2,
            AmmoEntryPrimitive spread3,
            AmmoEntryPrimitive cluster1,
            AmmoEntryPrimitive cluster2,
            AmmoEntryPrimitive cluster3,
            AmmoEntryPrimitive wyvern,
            AmmoEntryPrimitive sticky1,
            AmmoEntryPrimitive sticky2,
            AmmoEntryPrimitive sticky3,
            AmmoEntryPrimitive slicing,
            AmmoEntryPrimitive flaming,
            AmmoEntryPrimitive water,
            AmmoEntryPrimitive freeze,
            AmmoEntryPrimitive thunder,
            AmmoEntryPrimitive dragon,
            AmmoEntryPrimitive poison1,
            AmmoEntryPrimitive poison2,
            AmmoEntryPrimitive paralysis1,
            AmmoEntryPrimitive paralysis2,
            AmmoEntryPrimitive sleep1,
            AmmoEntryPrimitive sleep2,
            AmmoEntryPrimitive exhaust1,
            AmmoEntryPrimitive exhaust2,
            AmmoEntryPrimitive recover1,
            AmmoEntryPrimitive recover2,
            AmmoEntryPrimitive demon,
            AmmoEntryPrimitive armor,
            AmmoEntryPrimitive tranq
        )
        {
            Normal1 = normal1;
            Normal2 = normal2;
            Normal3 = normal3;
            Pierce1 = pierce1;
            Pierce2 = pierce2;
            Pierce3 = pierce3;
            Spread1 = spread1;
            Spread2 = spread2;
            Spread3 = spread3;
            Cluster1 = cluster1;
            Cluster2 = cluster2;
            Cluster3 = cluster3;
            Wyvern = wyvern;
            Sticky1 = sticky1;
            Sticky2 = sticky2;
            Sticky3 = sticky3;
            Slicing = slicing;
            Flaming = flaming;
            Water = water;
            Freeze = freeze;
            Thunder = thunder;
            Dragon = dragon;
            Poison1 = poison1;
            Poison2 = poison2;
            Paralysis1 = paralysis1;
            Paralysis2 = paralysis2;
            Sleep1 = sleep1;
            Sleep2 = sleep2;
            Exhaust1 = exhaust1;
            Exhaust2 = exhaust2;
            Recover1 = recover1;
            Recover2 = recover2;
            Demon = demon;
            Armor = armor;
            Tranq = tranq;
        }

        public static AmmoTableEntryPrimitive Read(Reader reader)
        {
            var normal1 = AmmoEntryPrimitive.Read(reader);
            var normal2 = AmmoEntryPrimitive.Read(reader);
            var normal3 = AmmoEntryPrimitive.Read(reader);
            var pierce1 = AmmoEntryPrimitive.Read(reader);
            var pierce2 = AmmoEntryPrimitive.Read(reader);
            var pierce3 = AmmoEntryPrimitive.Read(reader);
            var spread1 = AmmoEntryPrimitive.Read(reader);
            var spread2 = AmmoEntryPrimitive.Read(reader);
            var spread3 = AmmoEntryPrimitive.Read(reader);
            var cluster1 = AmmoEntryPrimitive.Read(reader);
            var cluster2 = AmmoEntryPrimitive.Read(reader);
            var cluster3 = AmmoEntryPrimitive.Read(reader);
            var wyvern = AmmoEntryPrimitive.Read(reader);
            var sticky1 = AmmoEntryPrimitive.Read(reader);
            var sticky2 = AmmoEntryPrimitive.Read(reader);
            var sticky3 = AmmoEntryPrimitive.Read(reader);
            var slicing = AmmoEntryPrimitive.Read(reader);
            var flaming = AmmoEntryPrimitive.Read(reader);
            var water = AmmoEntryPrimitive.Read(reader);
            var freeze = AmmoEntryPrimitive.Read(reader);
            var thunder = AmmoEntryPrimitive.Read(reader);
            var dragon = AmmoEntryPrimitive.Read(reader);
            var poison1 = AmmoEntryPrimitive.Read(reader);
            var poison2 = AmmoEntryPrimitive.Read(reader);
            var paralysis1 = AmmoEntryPrimitive.Read(reader);
            var paralysis2 = AmmoEntryPrimitive.Read(reader);
            var sleep1 = AmmoEntryPrimitive.Read(reader);
            var sleep2 = AmmoEntryPrimitive.Read(reader);
            var exhaust1 = AmmoEntryPrimitive.Read(reader);
            var exhaust2 = AmmoEntryPrimitive.Read(reader);
            var recover1 = AmmoEntryPrimitive.Read(reader);
            var recover2 = AmmoEntryPrimitive.Read(reader);
            var demon = AmmoEntryPrimitive.Read(reader);
            var armor = AmmoEntryPrimitive.Read(reader);
            reader.Offset(6); // Skip unknonw1 and unknown2.
            var tranq = AmmoEntryPrimitive.Read(reader);

            return new AmmoTableEntryPrimitive(
                normal1,
                normal2,
                normal3,
                pierce1,
                pierce2,
                pierce3,
                spread1,
                spread2,
                spread3,
                cluster1,
                cluster2,
                cluster3,
                wyvern,
                sticky1,
                sticky2,
                sticky3,
                slicing,
                flaming,
                water,
                freeze,
                thunder,
                dragon,
                poison1,
                poison2,
                paralysis1,
                paralysis2,
                sleep1,
                sleep2,
                exhaust1,
                exhaust2,
                recover1,
                recover2,
                demon,
                armor,
                tranq
            );
        }
    }
}
