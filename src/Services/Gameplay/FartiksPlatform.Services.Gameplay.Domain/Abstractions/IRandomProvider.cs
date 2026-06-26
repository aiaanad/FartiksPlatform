namespace FartiksPlatform.Services.Gameplay.Domain.Abstractions;

public interface IRandomProvider
{
    double NextDouble();
    int Next(int minValue, int maxValue);
}
