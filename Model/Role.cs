﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace Lms.Model;

[Table("t_m_role")]
[Index(nameof(RoleCode), IsUnique = true, Name = "role_bk")]
internal class Role : BaseModel
{
    [Column("role_code"), MaxLength(10)]
    public string RoleCode { get; set; }

    [Column("role_name"), MaxLength(20)]
    public string RoleName { get; set; }
}
