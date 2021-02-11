class NoteService {
    static apiURL = 'http://localhost:5000';

    static async GetNoteDetails(noteId) {
        try {
            const response = await fetch(`${NoteService.apiURL}/Notes/${noteId}`);
            return response.json();
        }
        catch(ex) {}
    }
    
    static async GetNoteList() {
        try {
            const response = await fetch(`${NoteService.apiURL}/Notes`);
            return response.json();
        }
        catch(ex) {}
    }

    static SortNotes(notes, sortProp, order) {
        const orderD = (order === 'desc');
        notes.sort((a,b) => {
            if (a[sortProp] < b[sortProp]) return orderD ? 1 : -1;
            return  a[sortProp] > b[sortProp] ? (orderD ? -1 : 1) : 0;
        });
        return notes;
    }
}

class TableHandler {

    constructor() {
        this.notes = [];
        this.sortSettings = {
            property : 'title',
            order : 'asc'
        };
        this.BindNoteList();
        this.BindInit();
        this.BindHeader();
    }

    BindInit() {
        window.addEventListener('load', async  e => {
            const fetchedNotes = await NoteService.GetNoteList();
            if(fetchedNotes == null) return; 
            this.notes = NoteService.SortNotes(fetchedNotes, 
                this.sortSettings.property,
                this.sortSettings.order);
            this.FillNoteList(this.notes);
        });
    }

    FillNoteList(notes) {
        const tableBd = this.GetTableEl();
        tableBd.innerHTML = "";
        for(const note of notes) {
            const noteRow = this.CreateNoteRow(note);
            tableBd.appendChild(noteRow);
        }
    }
    
    BindHeader() {
        const tableHeader = document.querySelector('#notesTableHeader');
        tableHeader.addEventListener('click', e => {
             if(e.target.tagName.toLowerCase() === 'th'){
                const prop = e.target.textContent.toLowerCase();
                this.ChangeSortSettings(prop);
                console.log(this.notes, prop);
                this.notes = NoteService.SortNotes(this.notes, 
                    this.sortSettings.property,
                    this.sortSettings.order);
                this.FillNoteList(this.notes);
             }
        });
    }
    
    ChangeSortSettings(prop) {
        if(this.sortSettings.property !== prop) {
            this.sortSettings.property = prop;
            this.sortSettings.order = 'asc';
            return;
        }
        this.sortSettings.order = this.sortSettings.order === 'desc' ? 'asc' : 'desc';
    }
    
    BindNoteList() {
        const tableBd = this.GetTableEl();
        tableBd.addEventListener('click', async (e) => {
            if(!e.target.classList.contains('note-title')) return;
            const noteId = e.target.closest('tr').dataset.noteId;
            try {
                const noteDetails = await NoteService.GetNoteDetails(noteId);
                this.FillModal(noteDetails);
            } catch(ex) {}
        });
    }
    
    GetTableEl(){
        return document.querySelector('#notesTableBody');
    }
    
    FillModal(noteDetails) {
        const modalBodyEl = document.querySelector('#modalBody');
        modalBodyEl.textContent = noteDetails.lastVersion.content;
        const modalLabelEl = document.querySelector('#detailsModalLabel');
        modalLabelEl.textContent = noteDetails.lastVersion.title;
    }
    
    CreateNoteRow(noteData) {
        const rowEl = document.createElement('tr');
        const titleCell = document.createElement('td');
        titleCell.textContent = noteData.title;
        titleCell.classList.add('note-title','text-success');
        titleCell.setAttribute('data-bs-toggle', 'modal');
        titleCell.setAttribute('data-bs-target', '#detailsModal');
        rowEl.appendChild(titleCell);
        rowEl.appendChild(this.CreateDateCell(noteData.created));
        rowEl.appendChild(this.CreateDateCell(noteData.modified));
        rowEl.dataset.noteId = noteData.id;
        return rowEl;
    }
    
    CreateDateCell(date) {
        const dateCell = document.createElement('td');
        dateCell.textContent = new Date(date).toLocaleString();
        return dateCell;
    }
}

new TableHandler();