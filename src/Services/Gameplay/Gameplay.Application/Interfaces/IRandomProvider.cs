namespace Gameplay.Application.Interfaces;

public interface IRandomProvider
{
    int Next(int min, int max);
}
