using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFParentChildInheritExample;

[Table("Base")]
public abstract class BaseClass
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string PropertyOne { get; set; }
}

public class DerivedClassOne : BaseClass
{
    public ICollection<ChildClass> Children { get; set; }
}

public class DerivedClassTwo : BaseClass
{
    public ICollection<ChildClass> Children { get; set; }
}

public class DerivedClassThree : BaseClass
{
    public ChildClass Child { get; set; }
}

[Table("Children")]
[PrimaryKey(nameof(Id), nameof(ParentId))]
public class ChildClass
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ParentId { get; set; }
    public BaseClass ParentClass { get; set; }

    public string PropertyX { get; set; }
}