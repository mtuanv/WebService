import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Page/home/home.component';
import { BlogComponent } from './Page/blog/blog.component';
import { LoginComponent } from './Page/login/login.component';
import { SignupComponent } from './Page/signup/signup.component';
import { HeaderComponent } from './Page/shared/header/header.component';
import { FooterComponent } from './Page/shared/footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    BlogComponent,
    LoginComponent,
    SignupComponent,
    HeaderComponent,
    FooterComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
