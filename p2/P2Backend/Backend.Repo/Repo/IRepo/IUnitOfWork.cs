using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repo.Repo.IRepo
{
  public interface IUnitOfWork : IDisposable
  {
    IPostRepo Post { get; }
    void Save();

  }
}
