namespace wishlist.handlers;

using maripose;

class SettingsHandler : RequestHandler
{
    public override void Handle()
    {
        string password = _GET["password"];

        if (password == Config.password)
        {
            string html = """
                <html lang="en">
                    <head>
                        <meta charset="UTF-8">
                        <meta http-equiv="X-UA-Compatible" content="IE=edge">
                        <meta name="viewport" content="width=device-width, initial-scale=1.0">

                        <title>wishlist</title>

                        <link rel="stylesheet" href="css/main.css">

                        <style>
                            #actions {
                                margin-top: 80px;
                            }
                            #actions p {
                                color: #333;
                                width: 80px;
                                margin-left: 50%;
                                transform: translate(-50%);
                            }
                            .action {
                                cursor: pointer;
                                margin-top: 5px;
                            }
                            .action:hover {
                                text-decoration: underline;
                            }
                            #main {
                                margin-top: 10px;
                            }
                            .progress-text {
                                margin-bottom: 20px;
                            }
                        </style>
                    </head>

                    <body>
                        <header>
                            <h1 id="title">Wishlist</h1>
                        </header>

                        <div id="actions">
                            <p class="action" onclick="addItem()">add item</p>
                        </div>
                        <div id="main"></div>

                        <script>
                            window.addEventListener('load', async () =>
                            {
                                let nameRes = await fetch('/name');
                                let name = await nameRes.text();

                                document.getElementById('title').innerText = `${name}'s wishlist`;

                                let itemsRes = await fetch('/wishlist');
                                let wishlist = await itemsRes.text();

                                let main = document.getElementById('main');
                                let lines = wishlist.split('\n');

                                for (let line of lines)
                                {
                                    if (line == '') continue;

                                    let data = line.split('|');
                                    let name = data[0];
                                    let goal = parseFloat(data[1]);
                                    let completed = parseFloat(data[2]);
                                    let link = data[3];

                                    let card = document.createElement('div');
                                    card.className = 'card';

                                    let title = document.createElement('h3');
                                    title.innerText = name;
                                    title.onclick = () => { window.open(link, '_blank') };

                                    let progressBar = document.createElement('div');
                                    progressBar.className = 'progress-bar';

                                    let progress = document.createElement('div');
                                    progress.style.width = `${completed / goal * 100}%`;
                                    progressBar.appendChild(progress);

                                    let progressText = document.createElement('div');
                                    progressText.className = 'progress-text';

                                    if (completed / goal == 1) progressText.innerText = `Complete!`;
                                    else progressText.innerText = `$${completed} / $${goal}`;

                                    let removeAction = document.createElement('p');
                                    removeAction.innerText = 'remove';
                                    removeAction.className = 'action';
                                    removeAction.onclick = async () => {
                                        location.assign(`/remove${location.search}&name=${name}`);
                                    };

                                    let updateCompletedAction = document.createElement('p');
                                    updateCompletedAction.innerText = 'update progress';
                                    updateCompletedAction.className = 'action';
                                    updateCompletedAction.onclick = async () => {
                                        completed = prompt('Enter updated completed value: ', completed);

                                        location.assign(`/progress${location.search}&name=${name}&completed=${completed}`);
                                    };

                                    card.appendChild(title);
                                    card.appendChild(progressBar);
                                    card.appendChild(progressText);
                                    card.appendChild(removeAction);
                                    card.appendChild(updateCompletedAction);

                                    main.appendChild(card);
                                }
                            });

                            function addItem()
                            {
                                let name = prompt('Enter item name: ');
                                let goal = prompt('Enter item amount: ');
                                let link = prompt('Enter link to item, if applicable: ');
                                
                                location.assign(`/add${location.search}&name=${name}&goal=${goal}&link=${link}`);
                            }
                        </script>
                    </body>
                </html>
            """;
            Write(html);
            Context.Response.Close();
        }
        else
        {
            Context.Response.Redirect("/");
            Context.Response.Close();
        }
    }
}