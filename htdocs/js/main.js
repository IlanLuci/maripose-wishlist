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

        card.appendChild(title);
        card.appendChild(progressBar);
        card.appendChild(progressText);

        main.appendChild(card);
    }
});

function login()
{
    let password = prompt('Enter the admin password to access the settings dashboard:');

    window.open(`/settings?password=${password}`, '_blank');
}