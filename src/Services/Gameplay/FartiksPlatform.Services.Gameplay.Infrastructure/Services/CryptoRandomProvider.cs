using System.Security.Cryptography;
using FartiksPlatform.Services.Gameplay.Domain.Abstractions;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Services;

public class CryptoRandomProvider : IRandomProvider
{
    public double NextDouble()
    {
        var bytes = RandomNumberGenerator.GetBytes(8);
        ulong ulongValue = BitConverter.ToUInt64(bytes, 0);

        return (double)(ulongValue & 0x001FFFFFFFFFFFFFUL) / (1UL << 53);
    }

    public int Next(int minValue, int maxValue)
    {
        if (minValue >= maxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(minValue), "Минимальное значение должно быть меньше максимального.");
        }

        long range = (long)maxValue - minValue;
        return (int)(minValue + (long)(NextDouble() * range));
    }
}
