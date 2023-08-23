using CSSL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CSSL.Experiments;

internal partial class ParentShader : CSShader
{

}

internal partial class OtherShader : CSShader
{

}


internal partial class MyShader : CSShader
{

    [Mixin]
    ParentShader parent = new();

    [Compose]
    OtherShader other = new();

    float amplitude;
    float frequency;
    float phase;

    [Stream]
    Vector3 color;
    [Stream]
    Vector3 position;

    [VSMain]
    public void Main()
    {
        color = new(1, 2, 3);
    }
}
