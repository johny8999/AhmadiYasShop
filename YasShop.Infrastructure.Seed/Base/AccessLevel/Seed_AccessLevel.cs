using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YasShop.Domain.Users.AccessLevelAgg.Contract;
using YasShop.Domain.Users.AccessLevelAgg.Entities;

namespace YasShop.Infrastructure.Seed.Base.AccessLevel;

public class Seed_AccessLevel : ISeed_AccessLevel
{
    private readonly ILogger _Logger;
    private readonly IAccessLevelRepository _AccessLevelRepository;

    public Seed_AccessLevel(ILogger logger, IAccessLevelRepository accessLevelRepository)
    {
        _Logger = logger;
        _AccessLevelRepository = accessLevelRepository;
    }

    public async Task<bool> RunAsync()
    {
        try
        {
            if (!await _AccessLevelRepository.GetNoTraking.AnyAsync(a => a.Name =="GeneralManager"))
            {
                await _AccessLevelRepository.AddAsync(new tblAccessLevel()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "GeneralManager",
                }, false);
            }

            if (!await _AccessLevelRepository.GetNoTraking.AnyAsync(a => a.Name == "NotConfirmedUser"))
            {
                await _AccessLevelRepository.AddAsync(new tblAccessLevel()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "NotConfirmedUser",
                }, false);
            }

            if (!await _AccessLevelRepository.GetNoTraking.AnyAsync(a => a.Name == "ConfirmedUser"))
            {
                await _AccessLevelRepository.AddAsync(new tblAccessLevel()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "ConfirmedUser",
                },false);
            }
            await _AccessLevelRepository.SaveChangeAsync();
            return true;
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return false;
        }
    }
}

