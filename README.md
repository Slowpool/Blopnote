Blopnote allows to type a song translation from one language to another.

How to use:
1. Pick a directory where translations should be saved
<img width="945" height="536" alt="image" src="https://github.com/user-attachments/assets/c05159b7-ffb8-4652-9958-d51f5d7663e1" />

2. Create new file
<img width="237" height="130" alt="image" src="https://github.com/user-attachments/assets/1d675133-b8ae-419b-9e76-b16767dfa2c0" />

3. Fill song info
<img width="737" height="748" alt="image" src="https://github.com/user-attachments/assets/6fa52a22-c48a-4782-88cb-99f755e94c7c" />

4. Translate!
<img width="943" height="538" alt="image" src="https://github.com/user-attachments/assets/ded52912-9109-433f-9f1a-a16268989833" />

Incredible features & why to use `Blopnote` instead of standard Notepad:
- Auto-pulling of the lyrics from `Genius.com` in one click:
// TODO gif/picture
Selenium does all the work under the hood.

- Your translation and lyrics are in one convenient window (you can hide lyrics)

- Auto-filling of song's structure lines: empty lines, lines which are bracketed by `[]` (`genius.com`-oriented feature):
// TODO gif

- Auto-completion of already translated lines. You don't need to copy these lines manually.
// TODO gif

- You'll never loose the focus because the current line is highlighted with blue color

- Have forgotten some word's translation and web translate is far from you? Just press `TAB` to skim the translation of current line pulled from google translate:
// TODO gif
Selenium pasts the lyrics of song to google translate on song creating and parses translation, so it's displayed instantly.

- Autosave. No need in annoying `Ctrl + S` pressing after each word like in default Notepad.
- 

More details:
User can past own lyrics (preferable ones are from https://genius.com), or app may parse lyrics automatically by built-in selenium parser.
In other words, this is a notepad-like app but with auto-save (each 5 seconds under the hood) and with lyrics in right part of screen. 
If somebody doesn't know, song translation is fun and it's not discussed.

Hotkeys:
`Ctrl + C` to copy current line
`Ctrl + W` to close app (like in standard windows' notepad)

