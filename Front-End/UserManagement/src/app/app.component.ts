import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AxiosService } from '../service/axios.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  users: any[] = [];

  constructor(private axiosService: AxiosService) {}

  ngOnInit() {
    this.axiosService.getUsers().then(response => {
      this.users = response.data.$values;
      console.log(this.users)
    }).catch(error => {
      console.error('Error al obtener usuarios:', error);
    });
  }
}
