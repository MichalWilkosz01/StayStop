import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { AuthGuard } from './guards/auth-guard.guard';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { HotelsComponent } from './hotels/hotels.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HotelRowComponent } from './hotel-row/hotel-row.component';
import {MatCardModule} from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatGridListModule} from '@angular/material/grid-list';
import { DetailsComponent } from './hotels/details/details.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatDividerModule} from '@angular/material/divider';
import {MatIconModule} from '@angular/material/icon';
import { RoomDetailsComponent } from './hotels/room-details/room-details.component';
import { MatButtonModule } from '@angular/material/button';
import { RegisterComponent } from './register/register.component';
import { AccountComponent } from './account/account.component';
import {MatTableModule} from '@angular/material/table';
import { HotelManagementComponent } from './hotels/hotel-management/hotel-management.component';
import { ManageHotelComponent } from './hotels/hotel-management/manage-hotel/manage-hotel.component';
import { HotelManagementRowComponent } from './hotels/hotel-management/hotel-management-row/hotel-management-row.component';
import { ManageRoomComponent } from './hotels/hotel-management/manage-hotel/manage-room/manage-room.component';
import { AssignManagerFormComponent } from './hotels/hotel-management/manage-hotel/assign-manager-form/assign-manager-form.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { SingleFileUploadComponent } from './single-file-upload/single-file-upload.component';
import { MultipleFileUploadComponent } from './multiple-file-upload/multiple-file-upload.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ReactiveFormsModule } from '@angular/forms';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { AddRoomComponent } from './hotels/add-room/add-room.component';
import { AddHotelComponent } from './hotels/add-hotel/add-hotel.component';
import { BasketComponent } from './basket/basket.component';
import { UserReservationHistoryComponent } from './user-reservation-history/user-reservation-history.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
import { AddOpinionComponent } from './opinions/add-opinion/add-opinion.component';
import { HotelOpinionsComponent } from './opinions/hotel-opinions/hotel-opinions.component';
import { UserOpinionsComponent } from './opinions/user-opinions/user-opinions.component';
import { UpdateOpinionComponent } from './opinions/update-opinion/update-opinion.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AdminReservationDetailsComponent } from './admin-panel/admin-reservation-details/admin-reservation-details.component';





export function tokenGetter() { 
  return localStorage.getItem("accessToken"); 
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HotelsComponent,
    HotelRowComponent,
    DetailsComponent,
    RoomDetailsComponent,
    RegisterComponent,
    AccountComponent,
    HotelManagementComponent,
    ManageHotelComponent,
    HotelManagementRowComponent,
    ManageRoomComponent,
    AssignManagerFormComponent,
    SingleFileUploadComponent,
    MultipleFileUploadComponent,
    AddHotelComponent,
    AddRoomComponent,
    BasketComponent,
    UserReservationHistoryComponent,
    ReservationDetailsComponent,
    AddOpinionComponent,
    HotelOpinionsComponent,
    UserOpinionsComponent,
    UpdateOpinionComponent,
    AdminPanelComponent,
    AdminReservationDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatCardModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatSelectModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatProgressBarModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5080"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [AuthGuard, provideAnimationsAsync()],
  bootstrap: [AppComponent]
})
export class AppModule { }
