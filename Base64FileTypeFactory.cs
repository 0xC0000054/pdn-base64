﻿/////////////////////////////////////////////////////////////////////////////////
//
// Base64 FileType Plugin for Paint.NET
//
// This software is provided under the MIT License:
//   Copyright (c) 2013-2018, 2020, 2021, 2022 Nicholas Hayes
//
// See LICENSE.txt for complete licensing and attribution information.
//
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;

namespace Base64FileTypePlugin
{
    public sealed class Base64FileTypeFactory : IFileTypeFactory
    {
        public FileType[] GetFileTypeInstances()
        {
            return new FileType[] { new Base64FileType() };
        }
    }
}
