# ZAFMC Reports (Power BI project)

## What this is

`ZAFMCReports.pbip` is one Power BI Desktop project that replaces the 36 SSRS reports in `ZAFMCReports\` for viewing on this PC. It has a Home page plus one page per SSRS report. Nothing is published: there is no Power BI Service, gateway or Report Server, and no data leaves the machine. The SSRS reports are unchanged. The links on the website's Reports screen open these pages (see "Opening reports from the website").

The project has two parts:

- `ZAFMCReports.SemanticModel\`: an Import-mode model (TMDL) with 20 tables, each read from one `dbo` table in the ZION database. Only the columns the SSRS reports show are imported.
- `ZAFMCReports.Report\`: the report (PBIR), with Home and 36 report pages.

The database is only read. Power Query uses `Sql.Database` navigation, with no native SQL and no stored procedures.

## One-time setup

You need Power BI Desktop 2.150 or later. Go to File > Options and settings > Options > Preview features and tick these items if they are listed:

- "Power BI Project (.pbip) save option"
- "Store semantic model using TMDL format"
- "Store reports using enhanced metadata format (PBIR)"

Click OK and restart Desktop. If an item isn't listed, your version already has it switched on.

## Open

Double-click `ZAFMCReports.pbip`, or use File > Open > Browse and pick `ZAFMCReports.pbip`. The project can take a minute or two to open. On first open the visuals are empty and Desktop shows "Some of the tables have incomplete or no data", because no data has been loaded yet.

## First refresh (one-time credential step)

Click Home > **Refresh**. The first refresh asks for credentials:

1. SQL Server `JACOB\SQLEXPRESS2025`: choose **Windows** > **Use my current credentials** > **Connect**.
2. If an encryption prompt appears, click **OK**. The connection is local, so unencrypted is fine.
3. If a privacy-level prompt appears, choose **Organizational**. You can also turn the prompt off with File > Options and settings > Options > Global > Privacy > **Always ignore privacy levels**. Either is fine.

Desktop stores these credentials in your Windows profile, not in this folder. Your Windows account needs read access (`db_datareader`) on ZION.

You can also do the credential step without the project: open Power BI Desktop > Get data > SQL Server > server `JACOB\SQLEXPRESS2025` > OK > Windows > Use my current credentials > Connect, then close Desktop without saving.

## Once the database is back

This project was built while SQL Server was not installed on this PC, so it has not yet been refreshed against real data. When the database is available:

1. Restore ZION on `JACOB\SQLEXPRESS2025`, or change the `SqlServer` / `SqlDatabase` parameters (see below).
2. Open `ZAFMCReports.pbip`.
3. Approve Windows credentials ("Use my current credentials").
4. Refresh.
5. Check each page's Records card against the old SSRS report or a `SELECT COUNT(*)` on the page's table.

## Refresh

Click Home > Refresh whenever you want current data. The data is a snapshot from the last refresh. Some values are worked out at refresh time, so refresh before reading these pages:

- ProjectsCompleted, ProjectsInProgress and ProjectsOverdue, and the Projects status donut (they compare dates with today).
- Male, Female and Children (they keep only members whose Date Appointed is on or before today).
- The Congregation `Age` used by Children.

## Using the report

- Start on **Home** and click a button to open a report. In Desktop's editing view a button needs **Ctrl+Click**. In Reading view (View > Reading view) a plain click works.
- Each page has the SSRS title, a table with the SSRS columns in SSRS order, a Records card, a **Home** button and a **Clear filters** button.
- Slicers replace the SSRS parameters. With nothing selected, everything is shown. Date slicers are ranges.
- The ten Church* grouping pages also have a bar chart, and Projects has a status donut.

## Changing the server or database

Click Transform data > Manage parameters. Set `SqlServer` (default `JACOB\SQLEXPRESS2025`) and `SqlDatabase` (default `ZION`), click OK, then Close & Apply, then Refresh.

## Saving and Git

- Press **Clear filters** on each page you used before saving, so no member names end up in the project files.
- Git ignores `.pbi\cache.abf` (your data) and `.pbi\localSettings.json` (rules in the repo's root `.gitignore`). Never force-add them, and don't save `.pbix` copies in this folder (`*.pbix` under `PowerBI\` is ignored too, because a PBIX embeds data).
- Expect Desktop to add lineage tags and diagram files on the first save. That diff is normal.
- Before committing, run this check command in Windows PowerShell from the repo root. It lists only the values saved inside filters and slicer selections. Formatting values such as fonts and titles are skipped, so the list is short.

  ```powershell
  git diff --stat -- PowerBI
  function Get-FilterLiterals($node, [bool]$inWhere, [string]$path) {
    if ($node -is [System.Array]) {
      for ($i = 0; $i -lt $node.Count; $i++) { Get-FilterLiterals $node[$i] $inWhere "$path[$i]" }
    } elseif ($node -is [System.Management.Automation.PSCustomObject]) {
      foreach ($p in $node.PSObject.Properties) {
        $w = $inWhere -or ($p.Name -eq 'Where')
        if ($w -and $p.Name -eq 'Literal') { "{0}: {1}" -f $path, $p.Value.Value }
        else { Get-FilterLiterals $p.Value $w "$path.$($p.Name)" }
      }
    }
  }
  Get-ChildItem -Path PowerBI -Recurse -Include page.json, visual.json, report.json, *.bookmark.json |
    ForEach-Object { Get-FilterLiterals (Get-Content $_.FullName -Raw | ConvertFrom-Json) $false $_.FullName.Substring((Get-Location).Path.Length + 1) }
  ```

  Expected output: exactly 10 lines, each ending in `'Male'`, `'Female'`, `'Single'` or `true`. They are the hidden page filters on the Male, Female, Children and Projects* pages. If anything else appears, such as a name, ID or phone number, press **Clear filters** on that page in Desktop, save, and run the command again.

## Opening reports from the website

The report links on the website's Reports screen (`Reports.aspx`) open the matching page of this project in Power BI Desktop on this PC. A link looks like `zafmc-pbi:Congregation`. It only works on a PC where the links are registered.

1. Register the links once per Windows user. In Windows PowerShell, from this folder, run:

   ```powershell
   powershell -NoProfile -ExecutionPolicy Bypass -File .\Register-ZafmcReportLinks.ps1
   ```

   This writes only `HKCU\Software\Classes\zafmc-pbi` (no admin rights needed). Run it again if you move the repo folder.
2. The first time you click a report link, Chrome or Edge asks "Open Windows PowerShell?". Tick **Always allow** and click **Open**.
3. `Open-ZafmcReport.ps1` then opens the page. It accepts only the page names in this project and ignores anything else.

How it works:

- The repo files are never changed by a click. The launcher keeps a viewing copy in `%LOCALAPPDATA%\ZAFMC\PowerBI` and copies this project over it whenever the project in the repo has changed. The copy's `.pbi` folders are kept, so data you loaded (refreshed and saved) in the copy stays.
- The launcher sets the copy's start page to the page you clicked and opens the copy in Power BI Desktop. Refresh and save in that window to keep the data for next time.
- If Power BI Desktop already has the copy open, the launcher brings that window to the front and shows a message naming the page tab to click. It never closes Power BI Desktop. To have a link open straight on its page, close Power BI Desktop and click the link again.
- A Power BI Desktop window you opened yourself on `ZAFMCReports.pbip` in this folder is a separate window. The links don't use it.

To remove the links from this PC, run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\Register-ZafmcReportLinks.ps1 -Unregister
```

To make the links point at the SSRS reports again, set `ReportLinkMode` to `SSRS` in the website's `Web.config` (`<appSettings>`). The links then use `ReportServerBaseUrl` and open in a new tab. `PowerBIDesktop` is the default.

## Differences from SSRS

- **UserLogIn:** the OTP column is intentionally left out. Passwords and login IDs are not imported either.
- **Member files:** photos, ID/passport documents, barcode images and fingerprints are never imported, nor their file names or content types. The table on the Congregation pages shows the same text columns as SSRS.
- **Date parameters:** SSRS `<= date` parameters defaulted to today and hid future-dated rows (for example upcoming events). Power BI date slicers start on the full range and show everything; drag the end date to get the old view. Congregation's SSRS "Date Appointed" defaulted to the earliest appointment date; Power BI shows all members by default.
- **Blank values:** SSRS filters silently dropped rows with an empty value in a filtered column (Churches, the Congregation family and the Projects pages). Power BI keeps those rows and shows "(Blank)" in the slicers.
- **Identical rows:** a Power BI table collapses rows whose shown values are all identical. Congregation rows always differ by Membership Number. On other pages the Records card (a row count) stays correct even if two identical rows collapse.
- **Comma-count columns** (Branch Pastors and similar on the Church* pages): an empty value counts as 1, the same as the SSRS SQL.
- **Project status:** a project can be both completed and overdue under the SSRS rules. The flag pages keep that overlap; the single "Project Status" label in the donut shows Overdue first. ProjectsInProgress excludes projects that start exactly today, as SSRS did.
- **Project flag-page counts:** the SSRS footer counted project sites, which skips rows with no site. The Power BI card counts rows.
- **Prompts that filtered nothing in SSRS** (PassBigSunday, PassWedding, the Male/Female/Children "Date Appointed" and the Projects* date prompts) are working slicers here. With nothing selected, the result equals SSRS.
- **Card labels** follow the SSRS footer text, so ChurchZones and ChurchSection say "Zone(s)"/"Section(s)" but count churches, as SSRS did.
- **Not reproduced (print-only SSRS elements):** page-number footers, the copyright footer, the HQ address block and the ZAFMC logo.
- **Spelling:** SSRS titles and captions are kept as written, for example "Mens' Report", "Video Occassion" and "Manegerial Post".

## SSRS report → Power BI page

| SSRS file | Power BI page tab | Home button text | Home group |
|---|---|---|---|
| Congregation.rdl | Congregation | Congregation Report | Congregation & members |
| Male.rdl | Male | Mens' Report | Congregation & members |
| Female.rdl | Female | Females' Report | Congregation & members |
| Children.rdl | Children | Childrens' Report | Congregation & members |
| Leadership.rdl | Leadership | Leadership Report | Leadership, Administration & Deceased |
| Administration.rdl | Administration | Administration Report | Leadership, Administration & Deceased |
| Deceased.rdl | Deceased | Deceased Report | Leadership, Administration & Deceased |
| UserLogIn.rdl | UserLogIn | User LogIn Report | Users |
| Churches.rdl | Churches | Churches Report | Churches & leadership positions |
| ChurchProvince.rdl | ChurchProvince | Province Report | Churches & leadership positions |
| ChurchDistricts.rdl | ChurchDistricts | District Report | Churches & leadership positions |
| ChurchZones.rdl | ChurchZones | Zone Report | Churches & leadership positions |
| ChurchSection.rdl | ChurchSection | Section Report | Churches & leadership positions |
| ChurchPastors.rdl | ChurchPastors | Pastors Report | Churches & leadership positions |
| ChurchEvangelist.rdl | ChurchEvangelist | Evangelist Report | Churches & leadership positions |
| ChurchPreachers.rdl | ChurchPreachers | Preachers Report | Churches & leadership positions |
| ChurchProphets.rdl | ChurchProphets | Prophets Report | Churches & leadership positions |
| ChurchDecons.rdl | ChurchDecons | Decons Report | Churches & leadership positions |
| ChurchMembers.rdl | ChurchMembers | General Members Report | Churches & leadership positions |
| Passovers.rdl | Passovers | Passover Report | Passovers & events |
| PassBigSunday.rdl | PassBigSunday | Big Sundays Report | Passovers & events |
| PassGeneralMeeting.rdl | PassGeneralMeeting | General Meetings | Passovers & events |
| PassGraduation.rdl | PassGraduation | Graduation Report | Passovers & events |
| PassMemorial.rdl | PassMemorial | Memorial Report | Passovers & events |
| PassWedding.rdl | PassWedding | Wedding Report | Passovers & events |
| PassWomen.rdl | PassWomen | Women Report | Passovers & events |
| PassYouth.rdl | PassYouth | Youth Report | Passovers & events |
| Events.rdl | Events | Events Report | Passovers & events |
| MediaNews.rdl | MediaNews | Media News | Media |
| MediaSermon.rdl | MediaSermon | Media Sermon | Media |
| MediaTestimony.rdl | MediaTestimony | Media Testimony | Media |
| MediaVideo.rdl | MediaVideo | Media Video | Media |
| Projects.rdl | Projects | Projects Report | Projects |
| ProjectsCompleted.rdl | ProjectsCompleted | Projects Completed Report | Projects |
| ProjectsInProgress.rdl | ProjectsInProgress | Projects-InProgress Report | Projects |
| ProjectsOverdue.rdl | ProjectsOverdue | Projects Overdue Report | Projects |
