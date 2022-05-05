import {Component, OnInit, Output} from '@angular/core';
import {NotesFilter} from "../../models/notesFilter";

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss'],
})
export class AppHeaderComponent implements OnInit {
  @Output()
  noteFilter: NotesFilter;

  ngOnInit(): void {
  }
}
