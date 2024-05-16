﻿using System;

namespace MovieNight.Domain.Domain
{
    public class Leaflet
    {
        public Leaflet()
        {
            OldPrice = 0;
            NewPrice = 0;
            Name = string.Empty;
            CreatedAt = DateTime.UtcNow;
        }
        public Leaflet(string name, decimal offPercent, decimal oldPrice, decimal newPrice)
        {
            Name = name;
            OffPercent = offPercent;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Name { get; private set; }
        public decimal? OffPercent { get; set; }
        public decimal OldPrice { get; private set; }
        public decimal NewPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }

        public void SetName(string name)
        {
            Name = name.Trim('*');
        }

        public void AddPrice(decimal price)
        {
            if (OldPrice == 0)
            {
                OldPrice = price;
                return;
            }

            if (OldPrice < price)
            {
                NewPrice = OldPrice;
                OldPrice = price;
                return;
            }
            NewPrice = price;
        }
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name)) return false;
            if (!char.IsLetter(Name[0])) return false;
            if (NewPrice == 0 || OldPrice == 0) return false;
            return true;
        }
    }

}
