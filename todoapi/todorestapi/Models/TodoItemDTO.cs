﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todorestapi.Models
{
  public class TodoItemDTO
  {
    public int TodoItemDTOId { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
  }
}
