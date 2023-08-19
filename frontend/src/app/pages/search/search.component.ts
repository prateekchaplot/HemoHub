import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Title } from '@angular/platform-browser';
import { ConfirmationDialogComponent } from 'src/app/components/confirmation-dialog/confirmation-dialog.component';
import { DialogData } from 'src/app/models/dialog-data';
import { UserParameters } from 'src/app/models/user-parameters';
import { ApiService } from 'src/app/services/api.service';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  selectedBloodGroup: string = '';
  bloodGroups = ["A-", "A+", "B-", "B+", "AB-", "AB+", "O+", "O-"];
  
  displayedColumns: string[] = ['position', 'name', 'state', 'city', 'actions'];
  dataSource = new MatTableDataSource<UserElement>();

  pageIndex = 0;
  pageSize = 10;
  totalItems = 0;

  constructor(
    private appService: AppService,
    titleService: Title,
    private apiService: ApiService,
    private dialog: MatDialog,
    private snackbar: MatSnackBar) {
    this.appService.setUser();
    titleService.setTitle('Search - Hemohub');
  }

  onBloodGroupSelected() {
    this.pageIndex = 0;
    this.loadData();
  }

  onPageSelected(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  loadData() {
    var userParams: UserParameters = new UserParameters(this.selectedBloodGroup, this.pageIndex + 1, this.pageSize);
    this.apiService.fetchUsers(userParams).subscribe((response: Response) => {
      this.dataSource.data = response.items;
      this.totalItems = response.count;
    });
  }

  openDialog() {
    const dialogData: DialogData = {
      title: 'Confirm Blood Withdrawal Request',
      content: 'Are you sure you want to raise a blood withdrawal request?'
    };

    let dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Blood withdrawal request confirmed.');
        this.openSnackbar('Blood withdrawal request confirmed.');
      } else {
        console.log('Blood withdrawal request canceled.');
      }
    });
  }

  openSnackbar(message: string) {
    this.snackbar.open(message, 'Close', { duration: 2500 });
  }
}

export interface Response {
  items: UserElement[];
  count: number;
}

export interface UserElement {
  name: string;
  address: AddressElement;
}

export interface AddressElement {
  streetAddress: string;
  city: string;
  state: string;
}