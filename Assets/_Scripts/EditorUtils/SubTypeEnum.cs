using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Misc;
using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    public abstract class SubTypeEnum : Enumeration
    {
        public abstract Type GetBaseType();


    }
}