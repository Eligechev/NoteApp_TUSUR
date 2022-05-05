import {Component, Injectable, OnInit} from '@angular/core';
import {AppService} from './services/app.service';
import {NoteCategories, NoteViewModel} from './models/noteModel';
import {NotesFilter} from './models/notesFilter';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

@Injectable({
  providedIn: 'root',
})

export class AppComponent implements OnInit {
  title = 'Ntvp-frontend';

  public NoteModels: NoteViewModel[];
  public NotesFilter: NotesFilter;
  public loader: boolean;

  constructor(private Service: AppService) {
    this.NoteModels = new Array<NoteViewModel>();
  }

  ngOnInit(): void {
    this.LoadNotes();
  }

  public LoadNotes(): void {
    this.loader = false;
    this.Service.Get(this.NotesFilter).subscribe((data: any) => {
      this.ConvertToViewModel(data);
      this.loader = true;
    });
  }

  public EditNote(noteModel: NoteViewModel): void {
    this.Service.Put(noteModel).subscribe(() => this.LoadNotes());
  }

  public AddNote(noteModel: NoteViewModel): void {
    this.Service.Post(noteModel).subscribe(() => this.LoadNotes());
  }

  public DeleteNote(id: any): void {
    this.Service.Delete(id).subscribe(() => this.LoadNotes());
  }

  public DeleteAll(): void {
    this.Service.DeleteAll().subscribe(() => this.LoadNotes());
  }

  private ConvertToViewModel(resp: any): void {
    this.NoteModels = new Array<NoteViewModel>();
    resp.map(model => {
      const newNoteModel = new NoteViewModel();
      newNoteModel.Id = model.id;
      newNoteModel.NoteName = model.noteName;
      newNoteModel.NoteMessage = model.noteMessage;
      newNoteModel.NoteCategory = model.notesCategory;
      newNoteModel.CreationDate = model.creationTime;
      newNoteModel.ChangeDate = model.changeDate;
      this.NoteModels.push(newNoteModel);
    });
  }
}
