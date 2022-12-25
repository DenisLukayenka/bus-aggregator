using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Web.Infrastructure.Models.Country;

namespace Web.SPA.Providers
{
    public class MapService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ConcurrentDictionary<string, Map> _mapCache;

        public MapService(IWebHostEnvironment env)
        {
            this._environment = env;
            this._mapCache = new ConcurrentDictionary<string, Map>();
        }

        public Map GetOrAdd(string filePath)
        {
            if (this._mapCache.ContainsKey(filePath))
            {
                return this._mapCache[filePath];
            }

            var map = this._mapCache.GetOrAdd(filePath, (filePath) => BuildMapFromFile(filePath));

            return map;
        }

        private static Map BuildMapFromFile(string filePath)
        {
            var document = XDocument.Load(filePath);
            var root = document.Root;

            var map = new Map();

            map.Id = int.Parse(root!.Element("id")!.Value);
            map.Caption = root.Element("caption")!.Value;
            map.Description = root.Element("description")?.Value;
            map.Paths = new List<MapPath>();
            map.Cities = new List<City>();

            var pathsNode = root.Element("paths");
            var citiesNode = root.Element("cities");

            foreach (var p in pathsNode!.Elements("path"))
            {
                map.Paths.Add(new MapPath
                {
                    Value = p.Element("value")!.Value,
                    Style = p.Element("style")!.Value,
                });
            }

            foreach (var c in citiesNode!.Elements("city"))
            {
                var cityId = int.Parse(c.Element("id").Value);
                var indicatorNode = c.Element("indicator");
                var textNode = c.Element("text");
                var textSizeNode = textNode!.Element("size");
                var textPositionNode = textNode.Element("position");

                var indicatorSizeNode = indicatorNode!.Element("size");
                var indicatorPositionNode = indicatorNode.Element("position");

                var cityText = new CityText
                {
                    Aligh = textNode.Element("align")?.Value,
                    Caption = textNode.Element("caption")!.Value,
                    Size = textSizeNode?.Value != null ? int.Parse(textSizeNode.Value) : null,
                    Style = textNode.Element("style")?.Value,
                };

                if (textPositionNode != null && textPositionNode.HasElements)
                {
                    cityText.Position = new Position
                    {
                        X = int.Parse(textPositionNode.Element("x")!.Value),
                        Y = int.Parse(textPositionNode.Element("y")!.Value),
                    };
                }

                map.Cities.Add(new City
                {
                    Id = cityId,
                    Indicator = new CityIndicator
                    {
                        Style = indicatorNode.Element("style")?.Value,
                        Position = new Position
                        {
                            X = int.Parse(indicatorPositionNode!.Element("x")!.Value),
                            Y = int.Parse(indicatorPositionNode!.Element("y")!.Value),
                        },
                        Size = indicatorSizeNode?.Value != null ? int.Parse(indicatorSizeNode.Value) : null,
                    },
                    Text = cityText
                });

            }

            return map;
        }
    }
}
