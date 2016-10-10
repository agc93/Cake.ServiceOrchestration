IEnumerable<string> GetFrameworks(string s) {
    return s.Split(',', ';').Select(fr => fr.Trim());
}

List<NuSpecContent> GetContent(IEnumerable<string> frameworks, DirectoryPath libDir) {
    return frameworks.SelectMany(f => new[] { 
        new NuSpecContent() { Source = artifacts + "lib/" + f + "/Cake.ServiceOrchestration.dll", Target = "lib/" + f},
        new NuSpecContent() { Source = artifacts + "lib/" + f + "/Cake.ServiceOrchestration.xml", Target = "lib/" + f}
    }).ToList();
}

Func<SolutionProject, bool> IsProject = p => p.Type != "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";