import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NoteViewModel, NoteCategories } from '../../models/noteModel';

@Component({
  selector: 'app-footer',
  templateUrl: './app-footer.component.html'
})
export class AppFooterComponent implements OnInit {

  // tslint:disable-next-line:no-output-on-prefix
  @Output() onAdd: EventEmitter<NoteViewModel> = new EventEmitter();
  public noteViewModel: NoteViewModel;

  ngOnInit(): void {
  }

  constructor() {
      this.noteViewModel = new NoteViewModel();
      this.noteViewModel.NoteName = '';
      this.noteViewModel.NoteMessage = '';
      this.noteViewModel.NoteCategory = null;
      this.noteViewModel.Id = 0;
  }

  addPost(): void {
    if (this.noteViewModel.NoteName.trim() && this.noteViewModel.NoteMessage.trim()) {
      this.onAdd.emit(this.noteViewModel);
    }
  }
}
