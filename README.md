# Developer onboarding – CSharpier setup

Tento projekt používá **CSharpier** pro automatické formátování C# kódu.

Veškerá konfigurace je již součástí repozitáře:

- `.config/dotnet-tools.json` → definice lokálních .NET nástrojů
- `.csharpierrc` → konfigurace CSharpieru
- `.csharpierignore` → ignorované složky
- `.editorconfig` → pravidla pro analyzery

---

# 1. Požadavky

Než začneš:

- nainstalovaný .NET SDK 10+
- Git
- IDE Visual Studio

---

# 2. Klonování repozitáře

```bash id="repo-clone"
cd <tvůj-pracovní-adresář>
git clone https://github.com/adamec-code/CarReader.git
cd CarReader
```

# 3. Instalace CSharpieru a nastavení git hooků

Po klonování spusť následující příkaz pro instalaci CSharpieru jako lokálního nástroje a nastavení git hooků:

```bash id="csharpier-install"
 dotnet tool restore
 git config core.hooksPath .githooks
```

Pro ověření instalace spusť:
```bash id="csharpier-verify"
 dotnet tool list
```


# 4. Použití CSharpieru
Pro vynucení formátování kódu spusť:
```bash id="csharpier-format"
dotnet csharpier format .
```	

# 5. Integrace s IDE
V Visual Studiu nastav CSharpier jako externí nástroj pro automatické formátování při ukládání souborů.

- Nainstaluj rozšíření Cshaprier pro Visual Studio z [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=AdamecCode.CSharpier).
- Restartuj Visual Studio
- Otevři **Tools > Options > Csharpier > General**
- Nastav volbu **Reformat with Csharpier on save** na **True**.

