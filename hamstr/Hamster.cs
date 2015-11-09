﻿using System;

namespace hamstr
{
    public class Hamster : IComparable<Hamster>
    {
        private readonly int _mates;
        private readonly long _consumeTotal;

        public Hamster(int portion, int greed, int mates)
        {
            Portion = portion;
            Greed = greed;
            _mates = mates;
            _consumeTotal = GetConsumption(_mates);
        }

        public long Portion { get; }

        public long Greed { get; }

        public long GetConsumption(int mates)
        {
            return Portion + Greed * mates;
        }

        public int CompareTo(Hamster other)
        {
            //return GetConsumption(1).CompareTo(other.GetConsumption(1));

            //return _consumeTotal.CompareTo(other._consumeTotal);

            int result = Portion.CompareTo(other.Portion);

            if (result == 0)
                return other.Greed.CompareTo(Greed);

            return result;
        }

        public override string ToString()
        {
            return $"Portion: {Portion}, Greed: {Greed}";
        }
    }
}