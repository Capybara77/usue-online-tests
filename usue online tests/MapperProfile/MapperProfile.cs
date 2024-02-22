using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using AutoMapper;
using Test_Wrapper;
using usue_online_tests.Dto;
using usue_online_tests.Models;

namespace usue_online_tests.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(dto => dto.Role,
                expression => expression.MapFrom(user => user.Role.ToString()));
        CreateMap<ITest, TestDto>()
            .ForMember(dto => dto.Pictures,
                expression => expression.MapFrom(test => test.Pictures.Select(ConvertPictureToBase64)));
    }

    private string ConvertPictureToBase64(Image image)
    {
        var memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Png);
        byte[] data = new byte[memoryStream.Length];
        memoryStream.Position = 0;
        memoryStream.Read(data, 0, data.Length);
        var str = Convert.ToBase64String(memoryStream.ToArray());

        memoryStream.Dispose();
        image.Dispose();
        return str;
    }
}