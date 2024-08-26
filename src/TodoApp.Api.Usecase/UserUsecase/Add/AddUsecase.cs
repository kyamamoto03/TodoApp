﻿using Domain.Exceptions;
using Domain.UserModel;

namespace TodoApp.Api.Usecase.UserUsecase.Add;

public interface IAddUsecase
{
    Task ExecuteAsync(AddCommand addCommand);
}
public class AddUsecase(IUserRepository userRepository) : IAddUsecase
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task ExecuteAsync(AddCommand addCommand)
    {
        if (await _userRepository.IsExist(addCommand.Email) == true)
        {
            throw new TodoDoaminExceptioon("ユーザが存在しています");
        }

        await _userRepository.AddAsync(addCommand.UserId, addCommand.UserName, addCommand.Email);
        await _userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
