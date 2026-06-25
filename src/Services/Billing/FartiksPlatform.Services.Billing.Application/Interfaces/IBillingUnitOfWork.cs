namespace FartiksPlatform.Services.Billing.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
