﻿using System.Xml.Serialization;

namespace Serializers;

[XmlType("person")]
public class Person
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    [XmlElement("first-name")]
    public string? FirstName { get; set; }
    [XmlElement("last-name")]
    public string? LastName { get; set; }
    [XmlElement("age")]
    public int Age { get; set; }

    public override string ToString()
    {
        return $"[{Id}], {FirstName} {LastName} ({Age})";
    }
}
