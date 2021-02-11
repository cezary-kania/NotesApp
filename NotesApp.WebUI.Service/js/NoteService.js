export default class NoteService {
    apiURL = 'http://localhost:5000';
    constructor() {
        this.notes = await this.GetNoteList();
        console.log(this.notes);
    }

    async GetNoteDetails(noteId) {
        try {
            const response = await fetch(`${apiURL}/Notes/${noteId}`);
            return response.json();
        }
        catch(ex) {}
    }
    
    async GetNoteList() {
        try {
            const response = await fetch(`${apiURL}/Notes`);
            return response.json();
        }
        catch(ex) {}
    }

}