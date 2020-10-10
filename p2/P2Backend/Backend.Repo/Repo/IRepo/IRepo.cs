using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Repo.IRepo
{
  public interface IRepo<T> where T : class
  {
    T Get(int id);
    ValueTask<T> GetAsync(int id);

    ValueTask<List<T>> GetAllAsync();
  }
}
