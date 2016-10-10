IEnumerable<string> GetFrameworks(string s) {
    return s.Split(',', ';').Select(fr => fr.Trim());
}

List<NuSpecContent> GetContent(IEnumerable<string> frameworks, DirectoryPath libDir) {
    return frameworks.SelectMany(f => new[] { 
        new NuSpecContent() { Source = artifacts + "lib/" + f + "/Cake.ServiceOrchestration.dll", Target = "lib/" + f},
        new NuSpecContent() { Source = artifacts + "lib/" + f + "/Cake.ServiceOrchestration.xml", Target = "lib/" + f}
    }).ToList();
}