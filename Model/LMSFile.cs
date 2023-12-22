using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lms.Model;

[Table("t_m_file")]
internal class LMSFile : BaseModel
{
    [Column("file_content")]
    public string FileContent { get; set; }

    [Column("file_extension"), MaxLength(5)]
    public string FileExtension { get; set; }
}
