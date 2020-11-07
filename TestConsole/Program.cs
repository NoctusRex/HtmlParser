using System;
using HtmlParser;
using Newtonsoft.Json;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser HtmlParser = new Parser();
            string html = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <meta name=""description"" content=""alex-web.dev"">
    <link rel=""icon"" type=""image/png"" href=""favicon-32x32.png"" sizes=""32x32"">
    <!--Implement video meta data-->

    <!--New Meta data for WhatsApp-->
    <meta name=""description"" content=""Hi,  i'm Alex! I love turning coffee to code!☕"">
    <meta property=""og:title"" content=""alex-web.dev""/>
    <meta property=""og:url"" content=""https://www.alex-web.dev"" />
    <meta property=""og:description"" content=""Hi,  i'm Alex! I love turning coffee to code!☕"">
    <meta property=""og:image"" content=""/imgs/wametapic.png"">

    <!-- Open Graph / Facebook -->
    <meta property=""og:type"" content=""website"">
    <meta property=""og:url"" content=""https://www.alex-web.dev"">
    <meta property=""og:title"" content=""alex-web.dev"">
    <meta property=""og:description"" content=""Hi,  i'm Alex! I love turning coffee to code!☕"">
    <meta property=""og:image"" content=""/imgs/fbmetapic.png"">

    <!-- Twitter -->
    <meta property=""twitter:card"" content=""summary_large_image"">
    <meta property=""twitter:url"" content=""https://www.alex-web.dev"">
    <meta property=""twitter:title"" content=""alex-web.dev"">
    <meta property=""twitter:description"" content=""Hi,  i'm Alex! I love turning coffee to code!☕"">
    <meta property=""twitter:image"" content=""/imgs/twittermetapic.png"">

    <link rel=""stylesheet"" href=""style.css"">
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.0.0/animate.min.css""/>
    <title>alex-web.dev!</title>
    <script type=""text/javascript"">
        if (screen.width <= 699) {
        document.location = ""mobile.html"";}
    </script>
</head>
<body>
    <div class=""header"">
        <div class=""inner-header flex"">
            <h1>alex-web.<i class=""fab fa-dev""></i></h1>
        </div>
        <p class=""skills""> Hobby Webdeveloper | Qualified IT-Management assistant |  3D printing enthusiast</p>
        <p class=""about"" style=""font-family: Comic Sans MS; ""><i>""Hello! My name is Alex and in my spare time i like to convert coffee to code! I build websites but also do a lot of other cool projects, feel free to check them out!""</i></p>
        <div>
            <a href=""https://www.alex-web.dev/about/more.html""><img class=""animate__animated animate__fadeInUp pb"" src=""imgs/takoyakii.jpg"" ></a>
        </div>
        <div class =""endcard animate__animated animate__fadeInUp"">
            <div>
                <p style=""font-size:large;"">View my Portfolio&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
            </div>            
            <div>
                <p style=""font-size:large;"">Visit my GitHub</p>
            </div>
        </div>
        <a href=""https://www.alex-web.dev/about/more.html""><i class=""fas fa-id-card fa-9x animate__animated animate__fadeInUp card"">&nbsp;&nbsp;</i></a>
        <a href=""https://github.com/xtakoyakii/""><i class=""fab fa-github fa-9x animate__animated animate__fadeInUp github""></i></a>
        <br>
        <div class=""container"">
            <a href=""mailto:alex@platonov.email"" class=""button animate__animated animate__fadeInUp"">Contact me&nbsp;<i class=""far fa-envelope""></i></a>
            <span class=""animate__animated animate__fadeInUp"">©2020 alex-web.dev | ©Takolabs</span>
        </div>  
        <div>
            <svg class=""waves"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" viewBox=""0 24 150 28"" preserveAspectRatio=""none"" shape-rendering=""auto"">
                <defs>
                    <path id=""gentle-wave"" d=""M-160 44c30 0 58-18 88-18s 58 18 88 18 58-18 88-18 58 18 88 18 v44h-352z"" />
                </defs>
                <g class=""parallax"">
                    <use xlink:href=""#gentle-wave"" x=""48"" y=""0"" fill=""rgba(255,255,255,0.7"" />
                    <use xlink:href=""#gentle-wave"" x=""48"" y=""3"" fill=""rgba(255,255,255,0.5)"" />
                    <use xlink:href=""#gentle-wave"" x=""48"" y=""5"" fill=""rgba(255,255,255,0.3)"" />
                    <use xlink:href=""#gentle-wave"" x=""48"" y=""7"" fill=""#fff"" />
                </g>
            </svg>
        </div>
    </div>
    <script src=""main.js""></script>
    <script src=""https://kit.fontawesome.com/18267d4626.js"" crossorigin=""anonymous""></script>
</body>
</html>";

            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(HtmlParser.ParseRawHtml(html), Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();

        }
    }
}
