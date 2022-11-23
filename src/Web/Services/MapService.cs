using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using WebSPA.Models.Map;

namespace WebSPA.Services
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

        public Map GetOrAdd(string id)
        {
            if (this._mapCache.ContainsKey(id))
            {
                return this._mapCache[id];
            }

            var map = this._mapCache.GetOrAdd(id, (id) => BuildMapFromFile(id));

            return map;
        }

        private Map BuildMapFromFile(string id)
        {
            string fileName = Path.ChangeExtension(id.ToLowerInvariant(), "xml");
            string path = Path.Combine(this._environment.WebRootPath, "maps", fileName);

            var document = XDocument.Load(path);
            var root = document.Root;

            var map = new Map();
            map.Id = root.Element("id").Value;
            map.Caption = root.Element("caption").Value;
            map.Description = root.Element("description")?.Value;
            map.Paths = new List<MapPath>();
            map.Cities = new List<City>();

            var pathsNode = root.Element("paths");
            var citiesNode = root.Element("cities");


            foreach (var p in pathsNode.Elements("path"))
            {
                map.Paths.Add(new MapPath
                {
                    Value = p.Element("value").Value,
                    Style = p.Element("style")?.Value,
                });
            }

            foreach (var c in citiesNode.Elements("city"))
            {
                var indicatorNode = c.Element("indicator");
                var textNode = c.Element("text");
                var textSizeNode = textNode.Element("size");
                var textPositionNode = textNode.Element("position");

                var indicatorSizeNode = indicatorNode.Element("size");
                var indicatorPositionNode = indicatorNode.Element("position");

                var cityText = new CityText
                {
                    Aligh = textNode.Element("align")?.Value,
                    Caption = textNode.Element("caption").Value,
                    Size = textSizeNode?.Value != null ? Convert.ToInt32(textSizeNode.Value) : null,
                    Style = textNode.Element("style")?.Value,
                };

                if (textPositionNode != null && textPositionNode.HasElements)
                {
                    cityText.Position = new Position
                    {
                        X = Convert.ToInt32(textPositionNode.Element("x").Value),
                        Y = Convert.ToInt32(textPositionNode.Element("y").Value),
                    };
                }

                map.Cities.Add(new City
                {
                    Indicator = new CityIndicator
                    {
                        Style = indicatorNode.Element("style")?.Value,
                        Position = new Position
                        {
                            X = Convert.ToInt32(indicatorPositionNode.Element("x").Value),
                            Y = Convert.ToInt32(indicatorPositionNode.Element("y").Value),
                        },
                        Size = indicatorSizeNode?.Value != null ? Convert.ToInt32(indicatorSizeNode.Value) : null,
                    },
                    Text = cityText
                });

            }

            return map;
        }
    }
}
