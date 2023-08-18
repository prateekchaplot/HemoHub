import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Observable, map, startWith } from 'rxjs';
import { UserIdAndName } from 'src/app/models/user';
import { ApiService } from 'src/app/services/api.service';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-exchange',
  templateUrl: './exchange.component.html',
  styleUrls: ['./exchange.component.css']
})
export class ExchangeComponent {
  myControl = new FormControl('');
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions: Observable<UserIdAndName[]>;
  selectedOption: UserIdAndName;

  constructor(public appService: AppService, private apiService: ApiService, titleService: Title) {
    appService.setUser();
    titleService.setTitle('Exchange - Hemohub');

    this.filteredOptions = new Observable();
    this.selectedOption = {
      id: '',
      name: ''
    };
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  onInput(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    if (inputValue.length >= 3) {
      console.log('calling');
      this.filteredOptions = this.apiService.fetchStudentIdAndNames(inputValue);
    }
  }

  onOptionSelected(option: UserIdAndName) {
    this.selectedOption = option;
  }
}
